import { useState } from "react";
import { formatViews } from "../../../utils/format-functions.ts";
import TextContent from "./text-content.tsx";

const Description = () => {
	const [descriptionOpened, setDescriptionOpened] = useState(false);

	const description = `📢 Добро пожаловать в наше полное руководство по разработке веб-сайтов на HTML, CSS и JavaScript!

👨‍💻 В этом видео мы погружаемся в мир веб-разработки, где шаг за шагом изучаем основы создания современных и адаптивных сайтов с нуля. Этот урок идеально подходит для начинающих, но также будет полезен для тех, кто хочет систематизировать свои знания и улучшить навыки.

📑 Что вас ждёт в этом видео:
1. Основы HTML (00:01:00)
Мы начнём с самого важного — языка разметки HTML, который используется для создания структуры веб-страниц. Вы научитесь:

Правильно организовывать HTML-документ
Использовать теги для добавления текста, изображений и ссылок
Понимать семантические элементы, которые улучшают SEO и доступность
2. Стилизация с помощью CSS (00:20:00)
Далее мы перейдём к CSS — каскадным таблицам стилей. Это мощный инструмент, который позволяет вам настраивать внешний вид сайта:

Подключение CSS к HTML
Основные свойства: цвета, шрифты, отступы, рамки
Работа с Flexbox и Grid для создания адаптивных макетов
Медиазапросы для правильного отображения сайта на любых устройствах
3. Добавление интерактивности с JavaScript (00:45:00)
Теперь займёмся программированием с использованием JavaScript. С его помощью вы сможете добавить интерактивные элементы на ваш сайт:

Основы синтаксиса и структуры JS
Работа с DOM для динамического изменения контента страницы
Реализация событий: обработка кликов, наведения и прокрутки
Введение в функции, массивы и циклы
4. Практическая часть — создание простого проекта (01:15:00)
Объединим всё, что изучили, в едином проекте! Мы создадим полноценную веб-страницу, используя все три технологии:

Разработка структуры страницы на HTML
Стилизация с помощью CSS
Добавление интерактивных элементов с JavaScript
5. Советы и рекомендации для начинающих веб-разработчиков (01:45:00)
В конце видео я поделюсь с вами важными рекомендациями, которые помогут вам улучшить ваши навыки и сделать сайты быстрее, красивее и удобнее:

Лучшая практика написания чистого кода
Оптимизация производительности сайта
Основы SEO для лучшего продвижения в поисковиках`;

	const handleDescription = () => {
		const button = document.getElementById("description-button");
		const description = document.getElementById("description");

		if (button && description) {
			const currentHeight = description.scrollHeight;

			if (!descriptionOpened) {
				button.textContent = "Свернуть";
				description.style.setProperty(
					"--expanded-height",
					`${currentHeight}px`,
				);
				description.classList.add("description-opened");
				setDescriptionOpened(true);
			} else {
				button.textContent = "Развернуть";
				description.classList.remove("description-opened");
				setDescriptionOpened(false);
			}
		}
	};

	return (
		<div className="video-description">
			<div className="video-description-text">
				<p>{formatViews(1645623, "views")}</p>
				<p>26 дек. 2023 г.</p>
			</div>
			<TextContent text={description} />
			<button onClick={handleDescription} id={"description-button"}>
				Развернуть
			</button>
		</div>
	);
};

export default Description;
