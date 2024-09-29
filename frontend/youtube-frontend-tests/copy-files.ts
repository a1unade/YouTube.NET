const fs = require('fs-extra');

const srcDir = '../youtube-frontend/src';
const destDir = './temp-src';

async function copyFiles() {
    try {
        await fs.copy(srcDir, destDir);
        console.log('Files copied successfully!');
    } catch (err) {
        console.error('Error copying files:', err);
    }
}

copyFiles();