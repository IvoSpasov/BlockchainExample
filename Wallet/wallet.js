$(document).ready(function () {
    function GenerateRandomWallet() {
        let ec = new elliptic.ec('secp256k1');
        let ripemd160 = new Hashes.RMD160();
        let keyPair = ec.genKeyPair();
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
        let wallet = GenerateRandomWallet();
        saveWalletToLocalStorage(wallet);
        $('#private-key').append(wallet.privateKey);
        $('#public-key').append(wallet.publicKey);
        $('#address').append(wallet.address);
    });

});