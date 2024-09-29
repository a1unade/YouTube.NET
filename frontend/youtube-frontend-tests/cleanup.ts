const fs = require('fs-extra');

const destDir = './temp-src';

async function cleanup() {
    try {
        await fs.remove(destDir);
        console.log('Temporary files removed!');
    } catch (err) {
        console.error('Error removing files:', err);
    }
}

// Запускаем функцию очистки
cleanup();