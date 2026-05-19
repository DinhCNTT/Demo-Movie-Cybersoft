import { Outlet } from 'react-router-dom'
import TempHeader from '../components/TempHeader/TempHeader'

export default function MainLayout() {
  return (
    <div className="flex min-h-screen flex-col bg-gray-50">
      <TempHeader />
      <main className="flex-1">
        <Outlet />
      </main>
    </div>
  )
}
