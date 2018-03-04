$(document).ready(function () {
    let ec = new elliptic.ec('secp256k1');

    function generateRandomWallet() {
        let keyPair = ec.genKeyPair();
        return getWalletFromKeyPair(keyPair);
    }

    function openExistingWallet(privateKey) {
        let keyPair = ec.keyFromPrivate(privateKey);
        return getWalletFromKeyPair(keyPair);
    }

    function getWalletFromKeyPair(keyPair) {
        let ripemd160 = new Hashes.RMD160();
        let privateKey = keyPair.getPrivate().toString('hex');
        let publicPoint = keyPair.getPublic();
        let publicKey = publicPoint.encode('hex');
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
});