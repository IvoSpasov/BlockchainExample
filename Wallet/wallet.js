$(document).ready(function () {
    populateSenderInput();
    let ec = new elliptic.ec('secp256k1');

    function generateRandomWallet() {
        let keyPair = ec.genKeyPair();
        return getWalletFromKeyPair(keyPair);
    }

    function openExistingWallet(privateKey) {
        let keyPair = ec.keyFromPrivate(privateKey, 'hex');
        return getWalletFromKeyPair(keyPair);
    }

    function getWalletFromKeyPair(keyPair) {
        let ripemd160 = new Hashes.RMD160();
        let privateKey = keyPair.getPrivate().toString('hex');
        let publicPoint = keyPair.getPublic();
        // let publicKey = publicPoint.encode('hex'); // non compressed
        let publicKeyCompressed = publicPoint.encodeCompressed('hex');
        let address = ripemd160.hex(publicKeyCompressed);

        return {
            privateKey: privateKey,
            publicKey: publicKeyCompressed,
            address: address
        }
    }

    function saveWalletToLocalStorage(wallet) {
        localStorage.wallet = JSON.stringify(wallet);
    }

    function getWalletFromLocalStorage() {
        return JSON.parse(localStorage.wallet);
    }

    function createTransactionJson(to, value) {
        let wallet = getWalletFromLocalStorage();

        return {
            from: wallet.address,
            to: to,
            senderPubKey: wallet.publicKey,
            value: value,
            fee: 10,
            dateCreated: new Date().toISOString()
        }
    }

    function signTransaction(transaction) {
        let transactionString = JSON.stringify(transaction);
        let sha256 = new Hashes.SHA256();
        let transactionHash = sha256.hex(transactionString);
        let wallet = getWalletFromLocalStorage();
        let keyPair = ec.keyFromPrivate(wallet.privateKey, 'hex');
        let signature = keyPair.sign(transactionHash);
        let signArr = [signature.r.toString('hex'), signature.s.toString('hex')];
        return signArr;
    }

    function populateSenderInput() {
        $('#sender').val(getWalletFromLocalStorage().address);
    }

    $('#new-wallet').click(() => {
        let wallet = generateRandomWallet();
        saveWalletToLocalStorage(wallet);
        $('#private-key').empty();
        $('#public-key').empty();
        $('#address').empty();
        $('#private-key').append(wallet.privateKey);
        $('#public-key').append(wallet.publicKey);
        $('#address').append(wallet.address);
        populateSenderInput();
    });

    $('#open-wallet').click(() => {
        let privateKey = $('#private-key-input').val();
        let wallet = openExistingWallet(privateKey);
        saveWalletToLocalStorage(wallet);
        $('#private-key-input').val('');
        $('#private-key2').empty();
        $('#public-key2').empty();
        $('#address2').empty();
        $('#private-key2').append(wallet.privateKey);
        $('#public-key2').append(wallet.publicKey);
        $('#address2').append(wallet.address);
        populateSenderInput();
    });

    $('#sign-transaction').click(() => {
        let to = $('#recipient').val();
        let value = $('#value').val();
        let transaction = createTransactionJson(to, value);
        let signature = signTransaction(transaction);
        transaction.senderSignature = signature;
        $('#signed-transaction').val(JSON.stringify(transaction, null, '\t'));
    });

    $('#send-transaction').click(() => {
        let transaction = $('#signed-transaction').text();
        $.ajax({
            url: 'http://localhost:57778/api/transactions/send',
            method: 'POST',
            data: transaction,
            contentType: 'application/json',
            dataType: 'json',
            success: function (result, status, xhr) {
                console.log(result);
            },
            error: function (xhr, status, error) {
                console.log(xhr);
            }
        });
    });
});