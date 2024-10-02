const fs = require('fs-extra');

const destDir = './temp-src';
const accDir = './acc-src';

async function cleanup() {
    try {
        await fs.remove(destDir);
        await fs.remove(accDir);
        console.log('Temporary files removed!');
    } catch (err) {
        console.error('Error removing files:', err);
    }
}

cleanup();