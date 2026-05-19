import { useSelector } from 'react-redux'

export default function HomePage() {
  const { user, bookingIds } = useSelector((state) => state.user)

  return (
    <div className="mx-auto max-w-6xl px-4 py-10">
      <h1 className="mb-2 text-3xl font-bold text-gray-900">
        Đặt vé xem phim
      </h1>
      <p className="text-gray-600">
        Chọn phim 
      </p>

      {user && (
        <div className="mt-8 rounded-lg border bg-white p-4">
          <h2 className="font-semibold text-gray-800">Tài khoản</h2>
          <p className="mt-1 text-sm text-gray-600">
            {user.hoTen} ({user.taiKhoan}) — {user.role}
          </p>
          {bookingIds.length > 0 && (
            <p className="mt-2 text-sm text-gray-500">
              Mã vé đã lưu: {bookingIds.join(', ')}
            </p>
          )}
        </div>
      )}
    </div>
  )
}
