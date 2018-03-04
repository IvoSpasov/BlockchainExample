$(document).ready(function () {
    function GenerateRandomWallet() {
        let ec = new elliptic.ec('secp256k1');
        let ripemd160 = new Hashes.RMD160();
        let keyPair = ec.genKeyPair();
        let privateKey = keyPair.getPrivate().toString('hex');
        let publicPoint = keyPair.getPublic();
        let publicKey = publicPoint.encode('hex'); 
        let address = ripemd160.hex(publicKey);
        
        console.log(privateKey);
        console.log(publicKey);
        console.log(address);
    }

    GenerateRandomWallet();
});