import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../../store/slices/userSlice';
import { useNavigate } from 'react-router-dom';

export default function Admin() {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { userInfo } = useSelector(state => state.user);

    const handleLogout = () => {
        dispatch(logout());
        navigate('/login');
    };

    return (
        <div className="min-h-screen bg-gray-100 flex flex-col items-center justify-center p-4">
            <div className="bg-white p-10 rounded-xl shadow-xl text-center max-w-lg w-full border-t-8 border-red-600">
                <h1 className="text-4xl font-black text-gray-800 mb-2">ĐÂY LÀ TRANG ADMIN</h1>
                <p className="text-lg text-gray-600 mb-8">Xin chào Quản trị viên: <span className="font-bold text-red-600">{userInfo?.hoTen}</span></p>
                <p className="mb-8 text-gray-500 italic">Tính năng quản lý phim và người dùng sẽ được thiết kế ở đây.</p>
                <button 
                    onClick={handleLogout}
                    className="w-full px-6 py-3 bg-gray-800 text-white font-bold rounded hover:bg-black transition-colors"
                >
                    ĐĂNG XUẤT
                </button>
            </div>
        </div>
    );
}
