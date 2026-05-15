import api from './api';

export const quanLyNguoiDungService = {
    dangNhap: (thongTinDangNhap) => {
        return api.post('/QuanLyNguoiDung/DangNhap', thongTinDangNhap);
    },
    dangKy: (thongTinDangKy) => {
        return api.post('/QuanLyNguoiDung/DangKy', thongTinDangKy);
    }
};
