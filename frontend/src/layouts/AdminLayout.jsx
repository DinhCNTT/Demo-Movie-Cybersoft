import { Navigate, Outlet, Link } from 'react-router-dom'
import { useSelector } from 'react-redux'
import TempHeader from '../components/TempHeader/TempHeader'

export default function AdminLayout() {
  const { user } = useSelector((state) => state.user)

  if (!user || user.role !== 'ADMIN') {
    return <Navigate to="/" replace />
  }

  return (
    <div className="min-h-screen bg-gray-100">
      <TempHeader />
      <div className="mx-auto flex max-w-6xl gap-6 px-4 py-6">
        <aside className="w-56 shrink-0 rounded-lg bg-white p-4 shadow">
          <p className="mb-4 text-xs font-semibold uppercase tracking-wide text-gray-400">
            Quản trị
          </p>
          <nav className="flex flex-col gap-1 text-sm">
            <Link
              to="/admin"
              className="rounded px-3 py-2 hover:bg-red-50 hover:text-red-600"
            >
              Tổng quan
            </Link>
          </nav>
        </aside>
        <div className="min-w-0 flex-1 rounded-lg bg-white p-6 shadow">
          <Outlet />
        </div>
      </div>
    </div>
  )
}
