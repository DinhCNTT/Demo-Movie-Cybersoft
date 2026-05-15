import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Admin from './pages/Admin/Admin';
import AdminRoute from './routes/AdminRoute';

function Home() {
  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-blue-50">
      <h1 className="text-5xl font-black text-blue-600 mb-4">TRANG CHỦ</h1>
      <p className="text-xl text-gray-600 mb-8">Dành cho mọi Khách Hàng đặt vé.</p>
      <div className="flex gap-4">
        <a href="/login" className="px-6 py-2 bg-blue-600 text-white font-bold rounded shadow hover:bg-blue-700">Đăng Nhập</a>
        <a href="/register" className="px-6 py-2 bg-white text-blue-600 font-bold rounded shadow border border-blue-600 hover:bg-blue-50">Đăng Ký</a>
      </div>
    </div>
  );
}

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        
        {/* Route được bảo vệ (Chỉ Admin mới được vào) */}
        <Route path="/admin" element={
          <AdminRoute>
            <Admin />
          </AdminRoute>
        } />
        
        {/* Bắt các link không tồn tại */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
