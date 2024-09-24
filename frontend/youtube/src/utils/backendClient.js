import axios from 'axios';

const backendClient = axios.create({
    baseURL: 'http://localhost:5041/api/'
});

export default backendClient;