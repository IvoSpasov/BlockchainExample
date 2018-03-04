$(document).ready(function () {
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
        let pointX = publicPoint.getX().toString('hex');
        let pointY = publicPoint.getY().isOdd() ? '1' : '0';
        let publicKey = pointX + pointY;
        let address = ripemd160.hex(publicKey);

        return {
            privateKey: privateKey,
            publicKey: publicKey,
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
        let transactionHash = sha256.hex(transactionString).split('');
        let wallet = getWalletFromLocalStorage();
        let keyPair = ec.keyFromPrivate(wallet.privateKey, 'hex');
        let signature = keyPair.sign(transactionHash);
        let signArr = [signature.r.toString('hex'), signature.s.toString('hex')];
        return signArr;
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
    });

    $('#sender').val(getWalletFromLocalStorage().address);

    $('#sign-transaction').click(() => {
        let to = $('#recipient').val();
        let value = $('#value').val();
        let transaction = createTransactionJson(to, value);
        let signature = signTransaction(transaction);
        transaction.senderSignature = signature;
    });
});