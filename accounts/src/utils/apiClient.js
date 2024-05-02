import axios from 'axios'

// Axios-клиент для запросов к API
export const apiClient = axios.create({
    baseURL: 'http://localhost:5041/api/'
});