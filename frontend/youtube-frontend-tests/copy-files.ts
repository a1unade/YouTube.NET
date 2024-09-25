const fs = require('fs-extra'); // Подключаем fs-extra для удобного копирования

const srcDir = '../youtube-frontend/src'; // Путь к исходной папке
const destDir = './temp-src'; // Временная папка для тестов

// Функция для копирования файлов
async function copyFiles() {
    try {
        await fs.copy(srcDir, destDir); // Копируем файлы
        console.log('Files copied successfully!');
    } catch (err) {
        console.error('Error copying files:', err);
    }
}

// Запускаем функцию копирования
copyFiles();