import axios from 'axios';

const apiKey = import.meta.env.VITE_YOUTUBE_API_KEY;

const apiClient = axios.create({
    baseURL: 'https://youtube.googleapis.com/youtube/v3'
});

// const axiosClient = axios.create({
//     baseURL : "http://localhost:5041/"
// });

apiClient.interceptors.request.use(config => {
    config.url += `&key=${apiKey}`;
    return config;
});

export default apiClient;