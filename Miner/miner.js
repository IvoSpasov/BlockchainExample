$(document).ready(function () {
    function calculate() {
        let nonce = 0;
        let dateCreated;
        let blockDataHash = 'AA5FB4AFB0154D2BDD3315E074F219351FDF13908F1C515E07BE12124A3D3760';
        let together = blockDataHash;
        let sha256 = new Hashes.SHA256();
        let resultHash;
        while (true) {
            dateCreated = new Date().toISOString();
            nonce++;
            together += dateCreated + nonce;
            resultHash = sha256.hex(together);

            if(checkForFoundHash(2, resultHash)){
                return resultHash;
            }
        }
    }

    function checkForFoundHash(difficulty, hash) {
        let zeros = Array(difficulty + 1).join('0');
        let firstChars = hash.substr(0, difficulty);
        let found = zeros == firstChars;
        return found;
    }

    $('#get-mining-job').click(() => {
        $.ajax({
            url: 'http://localhost:57778/api/mining/get-mining-job',
            method: 'GET',
            success: function (result, status, xhr) {

            },
            error: function (xhr, status, error) {
                console.log(xhr);
            }
        });
    });
});