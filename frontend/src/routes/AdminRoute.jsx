import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router-dom';

export default function AdminRoute({ children }) {
    const { userInfo } = useSelector(state => state.user);

    // Nếu chưa đăng nhập -> Đuổi về trang Đăng nhập
    if (!userInfo) {
        return <Navigate to="/login" />;
    }

    // Nếu đã đăng nhập nhưng KHÔNG PHẢI Quản trị -> Đuổi về trang Chủ
    if (userInfo.maLoaiNguoiDung !== 'QuanTri') {
        alert('Bạn không có quyền truy cập trang Quản Trị Viên!');
        return <Navigate to="/" />;
    }

    // Nếu đúng là Admin thì cho phép truy cập Component bên trong
    return children;
}
