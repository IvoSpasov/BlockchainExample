$(document).ready(function () {
    function GenerateRandomWallet() {
        let ec = new elliptic.ec('secp256k1');
        let keyPair = ec.genKeyPair();
        let privateKey = keyPair.getPrivate().toString('hex');
        let publicPoint = keyPair.getPublic();
        var publicKey = publicPoint.encode('hex');    
        
        console.log(privateKey);
        console.log(publicKey);
    }

    GenerateRandomWallet();
});