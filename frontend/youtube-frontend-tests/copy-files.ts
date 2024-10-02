const fs = require('fs-extra');

const srcDirFrontend = '../youtube-frontend/src';
const srcDirAccount = '../youtube-accounts/src';
const destDirFrontend = './temp-src';
const destDirAccount = './acc-src';

async function copyFiles() {
    try {
        await fs.copy(srcDirFrontend, destDirFrontend);
        console.log('Frontend files copied successfully!');

        await fs.copy(srcDirAccount, destDirAccount);
        console.log('Account files copied successfully!');
    } catch (err) {
        console.error('Error copying files:', err);
    }
}

copyFiles();
