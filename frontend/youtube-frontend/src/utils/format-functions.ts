export const formatDate = (date: string): string => {
	const currentDate: Date = new Date();
	const publishedDate: Date = new Date(date);
	const timeDifference: number =
		currentDate.getTime() - publishedDate.getTime();
	const secondsDifference = Math.floor(timeDifference / 1000);
	const minutesDifference = Math.floor(secondsDifference / 60);
	const hoursDifference = Math.floor(minutesDifference / 60);
	const daysDifference = Math.floor(hoursDifference / 24);
	const monthsDifference = Math.floor(daysDifference / 30);
	const yearsDifference = Math.floor(monthsDifference / 12);

	if (yearsDifference > 0) {
		return `${yearsDifference} ${yearsDifference === 1 ? "год" : "лет"} назад`;
	} else if (monthsDifference > 0) {
		return `${monthsDifference} ${monthsDifference === 1 ? "месяц" : "месяцев"} назад`;
	} else if (daysDifference > 0) {
		return `${daysDifference} ${daysDifference === 1 ? "день" : "дней"} назад`;
	} else if (hoursDifference > 0) {
		return `${hoursDifference} ${hoursDifference === 1 ? "час" : "часов"} назад`;
	} else if (minutesDifference > 0) {
		return `${minutesDifference} ${minutesDifference === 1 ? "минуту" : "минут"} назад`;
	} else {
		return `${secondsDifference} ${secondsDifference === 1 ? "секунду" : "секунд"} назад`;
	}
};

export const formatViews = (count: number, type: string) => {
	let titles = [""];

	if (type === "followers") {
		titles = ["подписчик", "подписчика", "подписчиков"];
	} else if (type === "views") {
		titles = ["просмотр", "просмотра", "просмотров"];
	} else if (type === "likes") {
		titles = ["", "", ""];
	}

	const cases = [2, 0, 1, 1, 1, 2];

	if (count < 1000 && count >= 0) {
		const index =
			count % 100 > 4 && count % 100 < 20
				? 2
				: cases[Math.min(count % 10, 5)];
		return `${count} ${titles[index]}`;
	} else if (count >= 1000 && count < 1000000) {
		let formattedCount = (count / 1000).toFixed(1);
		if (formattedCount.endsWith("0")) {
			formattedCount = formattedCount.slice(0, -2);
		} else {
			formattedCount = formattedCount.replace(".", ",");
		}
		return `${formattedCount} тыс. ${titles[2]}`;
	} else if (count >= 1000000 && count <= 999999999) {
		let formattedCount = (count / 1000000).toFixed(1);
		if (formattedCount.endsWith("00")) {
			formattedCount = formattedCount.slice(0, -3);
		} else if (formattedCount.endsWith("0")) {
			formattedCount = formattedCount.slice(0, -1).replace(".", ",");
		} else {
			formattedCount = formattedCount.replace(".", ",");
		}
		return `${formattedCount} млн. ${titles[2]}`;
	} else {
		return "";
	}
};
