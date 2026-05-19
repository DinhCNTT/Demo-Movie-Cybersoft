import { Link } from 'react-router-dom'
import { useSelector } from 'react-redux'
import { logout } from '../../utils/authStorage'
import logo from './../../assets/img/logo.png'
import { User, LogOut, LogIn, UserPlus } from "lucide-react";

export default function TempHeader() {
  const { user } = useSelector((state) => state.user)

  return (
    <header className="sticky top-0 z-40 bg-white text-black shadow">
      <div className="flex w-full items-center justify-between px-4 py-3">
        <Link to="/">
          <img src={logo} alt="Logo" width="200" />
        </Link>

        <nav className="hidden items-center gap-6 text-sm md:flex">
          <Link to="/" className="font-bold hover:text-red-400">
            Lịch Chiếu
          </Link>
          <Link to="/" className="font-bold hover:text-red-400">
            Cụm Rạp
          </Link>
          <Link to="/" className="font-bold hover:text-red-400">
            Tin Tức
          </Link>
          <Link to="/" className="font-bold hover:text-red-400">
            Ứng Dụng
          </Link>
          {user?.role === "ADMIN" && (
            <Link to="/admin" className="font-bold hover:text-red-400">
              Quản trị
            </Link>
          )}
        </nav>

        <div className="flex items-center gap-3">
          {user ? (
            <>
              <span className="hidden text-sm text-gray-600 sm:inline">
                Xin chào, <strong className="text-black">{user.hoTen}</strong>
              </span>
              <button
                type="button"
                onClick={logout}
                className="flex items-center gap-1.5 rounded border  bg-white px-3 py-1 text-[15px] font-semibold text-gray-700 hover:text-[#fb4626] transition-colors"
              >
                Đăng xuất
                <LogOut size={18} />
              </button>
            </>
          ) : (
            <>
              <Link
                to="/sign-in"
                className="flex items-center gap-1.5 rounded bg-white px-3 py-1 text-[15px] font-semibold text-gray-700 hover:text-[#fb4626] transition-colors"
              >
                <User size={18} />
                Đăng nhập
              </Link>

              <span className="text-gray-300">|</span>

              <Link
                to="/sign-up"
                className="flex items-center gap-1.5 rounded bg-white px-3 py-1 text-[15px] font-semibold text-gray-700 hover:text-[#fb4626] transition-colors"
              >
                <User size={18} />
                Đăng ký
              </Link>
            </>
          )}
        </div>
      </div>
    </header>
  );
}
