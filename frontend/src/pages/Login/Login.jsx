import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate, Link } from 'react-router-dom';
import { quanLyNguoiDungService } from '../../services/quanLyNguoiDungService';
import { setUserInfo } from '../../store/slices/userSlice';

export default function Login() {
    const [credentials, setCredentials] = useState({ taiKhoan: '', matKhau: '' });
    const [showPassword, setShowPassword] = useState(false);
    const [error, setError] = useState('');
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCredentials({ ...credentials, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await quanLyNguoiDungService.dangNhap(credentials);
            dispatch(setUserInfo(res.data));
            if (res.data.maLoaiNguoiDung === 'QuanTri') {
                navigate('/admin');
            } else {
                navigate('/');
            }
        } catch (err) {
            console.error("Lỗi đăng nhập:", err);
            if (err.response) {
                const data = err.response.data;
                setError(typeof data === 'string' ? data : "Tài khoản hoặc mật khẩu không đúng!");
            } else {
                setError("Lỗi mạng: Không thể kết nối đến Backend (Port 5052).");
            }
        }
    };

    return (
        <div className="min-h-screen relative flex items-center justify-center bg-cover bg-center font-sans" style={{ backgroundImage: "url('https://demo1.cybersoft.edu.vn/static/media/backapp.b46ef3a1.jpg')" }}>
            {/* Dark Overlay giống bản gốc */}
            <div className="absolute inset-0 bg-black/50"></div>
            
            <div className="relative z-10 bg-white p-10 rounded shadow-2xl w-full max-w-[420px]">
                {/* Header Icon (Hình tròn đỏ chứa icon user) */}
                <div className="flex flex-col items-center mb-6">
                    <div className="w-10 h-10 bg-[#fb4226] rounded-full flex items-center justify-center text-white mb-3 shadow-md">
                        <svg viewBox="0 0 24 24" fill="currentColor" className="w-6 h-6"><path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z"></path></svg>
                    </div>
                    <h2 className="text-2xl text-gray-800 font-medium tracking-wide">Đăng nhập</h2>
                </div>

                {error && <p className="text-red-500 text-center mb-4 text-sm font-medium">{error}</p>}

                <form onSubmit={handleSubmit} className="flex flex-col gap-5">
                    {/* Username Input */}
                    <div>
                        <input 
                            type="text" 
                            name="taiKhoan"
                            placeholder="Tài Khoản *" 
                            onChange={handleChange}
                            required
                            className="w-full px-4 py-3 bg-[#f8f9fa] border border-gray-300 rounded focus:outline-none focus:border-blue-500 focus:bg-white transition-colors text-gray-800"
                        />
                    </div>

                    {/* Password Input */}
                    <div className="relative">
                        <input 
                            type={showPassword ? "text" : "password"} 
                            name="matKhau"
                            placeholder="Mật Khẩu *" 
                            onChange={handleChange}
                            required
                            className="w-full px-4 py-3 bg-[#f8f9fa] border border-gray-300 rounded focus:outline-none focus:border-blue-500 focus:bg-white transition-colors text-gray-800 pr-12"
                        />
                        <button 
                            type="button" 
                            className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-800"
                            onClick={() => setShowPassword(!showPassword)}
                        >
                            {showPassword ? 
                                <svg viewBox="0 0 24 24" fill="currentColor" className="w-5 h-5"><path d="M12 4.5C7 4.5 2.73 7.61 1 12c1.73 4.39 6 7.5 11 7.5s9.27-3.11 11-7.5c-1.73-4.39-6-7.5-11-7.5zM12 17c-2.76 0-5-2.24-5-5s2.24-5 5-5 5 2.24 5 5-2.24 5-5 5zm0-8c-1.66 0-3 1.34-3 3s1.34 3 3 3 3-1.34 3-3-1.34-3-3-3z"></path></svg> : 
                                <svg viewBox="0 0 24 24" fill="currentColor" className="w-5 h-5"><path d="M12 7c2.76 0 5 2.24 5 5 0 .65-.13 1.26-.36 1.83l2.92 2.92c1.51-1.26 2.7-2.89 3.43-4.75-1.73-4.39-6-7.5-11-7.5-1.4 0-2.74.25-3.98.7l2.16 2.16C10.74 7.13 11.35 7 12 7zM2 4.27l2.28 2.28.46.46C3.08 8.3 1.78 10.02 1 12c1.73 4.39 6 7.5 11 7.5 1.55 0 3.03-.3 4.38-.84l.42.42L19.73 22 21 20.73 3.27 3 2 4.27zM7.53 9.8l1.55 1.55c-.05.21-.08.43-.08.65 0 1.66 1.34 3 3 3 .22 0 .44-.03.65-.08l1.55 1.55c-.67.33-1.41.53-2.2.53-2.76 0-5-2.24-5-5 0-.79.2-1.53.53-2.2zm4.31-.78l3.15 3.15.02-.16c0-1.66-1.34-3-3-3l-.17.01z"></path></svg>
                            }
                        </button>
                    </div>

                    {/* Remember me Checkbox */}
                    <div className="flex items-center">
                        <input type="checkbox" id="remember" className="w-4 h-4 mr-2 border-gray-300 rounded cursor-pointer accent-blue-600" />
                        <label htmlFor="remember" className="text-gray-700 cursor-pointer select-none text-sm">Nhớ tài khoản</label>
                    </div>

                    {/* Submit Button */}
                    <button 
                        type="submit" 
                        className="w-full bg-[#fb4226] text-white font-semibold py-3 rounded hover:bg-[#e03a20] transition-colors mt-2 text-sm uppercase tracking-wide shadow-md"
                    >
                        ĐĂNG NHẬP
                    </button>
                </form>

                <div className="mt-5 text-right">
                    <Link to="/register" className="text-blue-700 hover:text-blue-800 hover:underline font-medium text-sm">
                        Bạn chưa có tài khoản? Đăng ký
                    </Link>
                </div>
            </div>
        </div>
    );
}
