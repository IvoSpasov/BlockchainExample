$(document).ready(function () {
    function mineBlockHash(blockDataHash, difficulty) {
        let sha256 = new Hashes.SHA256();
        let nonce = 0;
        let dateCreated;
        let combinedInput;
        let minedBlockHash;
        while (true) {
            nonce++;
            dateCreated = new Date().toISOString();
            combinedInput = `${blockDataHash}${dateCreated}${nonce}`;
            minedBlockHash = sha256.hex(combinedInput);
            if (isBlockHashFound(difficulty, minedBlockHash)) {
                return {
                    minedBlockHash: minedBlockHash,
                    nonce: nonce,
                    timestamp: dateCreated
                }
            }
        }
    }

    function isBlockHashFound(difficulty, minedBlockHash) {
        let zeros = Array(difficulty + 1).join('0');
        let firstChars = minedBlockHash.substr(0, difficulty);
        return zeros == firstChars;
    }

    async function getMiningJob(minerAddress) {
        let result = await $.ajax({
            url: 'http://localhost:57778/api/mining/get-mining-job/' + minerAddress,
            method: 'GET',
            contentType: 'application/json',
            dataType: 'json',
            error: function (xhr, status, error) {
                console.log(xhr);
            }
        });

        return result;
    }

    $('#mine-a-block').click(async () => {
        let miningJob = await getMiningJob('abc34');
        if (miningJob.index) {
            let foundBlockInfo = mineBlockHash(result.blockDataHash, result.difficulty);
            console.log(foundBlockInfo);
        }
    });
});