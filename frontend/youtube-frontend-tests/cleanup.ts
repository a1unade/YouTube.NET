const fs = require('fs-extra');

const destDir = './temp-src'; // Временная папка для тестов

// Функция для очистки временной папки
async function cleanup() {
    try {
        await fs.remove(destDir); // Удаляем папку
        console.log('Temporary files removed!');
    } catch (err) {
        console.error('Error removing files:', err);
    }
}

// Запускаем функцию очистки
cleanup();