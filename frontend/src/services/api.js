import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5052/api', // Địa chỉ Backend
});

api.interceptors.request.use(config => {
    const userInfo = JSON.parse(localStorage.getItem('userInfo'));
    if (userInfo && userInfo.accessToken) {
        config.headers.Authorization = `Bearer ${userInfo.accessToken}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

export default api;
