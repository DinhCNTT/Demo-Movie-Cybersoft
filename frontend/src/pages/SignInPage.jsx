import { useEffect, useState } from 'react'
import { Link, Navigate, useLocation, useNavigate } from 'react-router-dom'
import { useDispatch, useSelector } from 'react-redux'
import { loginApi } from '../api/authApi'
import { applyLoginSuccess } from '../store/slices/userSlice'
import AuthPageShell from '../components/Auth/AuthPageShell'

const emptyLogin = { taiKhoan: '', matKhau: '' }

export default function SignInPage() {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const location = useLocation()
  const { user } = useSelector((state) => state.user)

  const [loginForm, setLoginForm] = useState(emptyLogin)
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    const prefill = location.state?.taiKhoan
    if (prefill) {
      setLoginForm((f) => ({ ...f, taiKhoan: prefill }))
    }
  }, [location.state])

  if (user) {
    return <Navigate to="/" replace />
  }

  const handleLogin = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      const res = await loginApi(loginForm)
      if (res.statusCode !== 200 || !res.content?.accessToken) {
        setError(res.message || 'Đăng nhập thất bại')
        return
      }
      applyLoginSuccess(dispatch, res.content)
      navigate('/', { replace: true })
    } catch (err) {
      setError(
        err.response?.data?.message || err.message || 'Đăng nhập thất bại',
      )
    } finally {
      setLoading(false)
    }
  }

  return (
    <AuthPageShell title="Đăng nhập">
      <p className="mb-4 text-sm text-gray-600">
        Chưa có tài khoản?{' '}
        <Link to="/sign-up" className="font-medium text-red-600 hover:underline">
          Đăng ký
        </Link>
      </p>  

      {error && (
        <p className="mb-3 rounded bg-red-50 px-3 py-2 text-sm text-red-600">
          {error}
        </p>
      )}

      <form onSubmit={handleLogin} className="space-y-4">
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Tài khoản</span>
          <input
            required
            autoComplete="username"
            className="w-full rounded border border-gray-300 px-3 py-2 outline-none focus:border-red-500"
            value={loginForm.taiKhoan}
            onChange={(e) =>
              setLoginForm({ ...loginForm, taiKhoan: e.target.value })
            }
          />
        </label>
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Mật khẩu</span>
          <input
            required
            type="password"
            autoComplete="current-password"
            className="w-full rounded border border-gray-300 px-3 py-2 outline-none focus:border-red-500"
            value={loginForm.matKhau}
            onChange={(e) =>
              setLoginForm({ ...loginForm, matKhau: e.target.value })
            }
          />
        </label>
        <button
          type="submit"
          disabled={loading}
          className="w-full rounded bg-red-600 py-2.5 font-medium text-white hover:bg-red-700 disabled:opacity-60"
        >
          {loading ? 'Đang xử lý...' : 'Đăng nhập'}
        </button>
      </form>
    </AuthPageShell>
  )
}
