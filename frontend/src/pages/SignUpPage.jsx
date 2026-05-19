import { useState } from 'react'
import { Link, Navigate, useNavigate } from 'react-router-dom'
import { useSelector } from 'react-redux'
import { registerApi } from '../api/authApi'
import AuthPageShell from '../components/Auth/AuthPageShell'

const emptyRegister = {
  taiKhoan: '',
  matKhau: '',
  hoTen: '',
  email: '',
  soDt: '',
  maNhom: 'GP01',
}

export default function SignUpPage() {
  const navigate = useNavigate()
  const { user } = useSelector((state) => state.user)

  const [registerForm, setRegisterForm] = useState(emptyRegister)
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  if (user) {
    return <Navigate to="/" replace />
  }

  const handleRegister = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      const res = await registerApi(registerForm)
      if (res.statusCode !== 200) {
        setError(res.message || 'Đăng ký thất bại')
        return
      }
      navigate('/sign-in', {
        replace: true,
        state: { taiKhoan: registerForm.taiKhoan },
      })
    } catch (err) {
      setError(err.response?.data?.message || err.message || 'Đăng ký thất bại')
    } finally {
      setLoading(false)
    }
  }

  return (
    <AuthPageShell title="Đăng ký">
      <p className="mb-4 text-sm text-gray-600">
        Đã có tài khoản?{' '}
        <Link to="/sign-in" className="font-medium text-red-600 hover:underline">
          Đăng nhập
        </Link>
      </p>

      {error && (
        <p className="mb-3 rounded bg-red-50 px-3 py-2 text-sm text-red-600">
          {error}
        </p>
      )}

      <form onSubmit={handleRegister} className="space-y-3">
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Tài khoản</span>
          <input
            required
            autoComplete="username"
            className="w-full rounded border border-gray-300 px-3 py-2"
            value={registerForm.taiKhoan}
            onChange={(e) =>
              setRegisterForm({ ...registerForm, taiKhoan: e.target.value })
            }
          />
        </label>
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Họ tên</span>
          <input
            required
            className="w-full rounded border border-gray-300 px-3 py-2"
            value={registerForm.hoTen}
            onChange={(e) =>
              setRegisterForm({ ...registerForm, hoTen: e.target.value })
            }
          />
        </label>
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Email</span>
          <input
            required
            type="email"
            autoComplete="email"
            className="w-full rounded border border-gray-300 px-3 py-2"
            value={registerForm.email}
            onChange={(e) =>
              setRegisterForm({ ...registerForm, email: e.target.value })
            }
          />
        </label>
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Số điện thoại</span>
          <input
            required
            autoComplete="tel"
            className="w-full rounded border border-gray-300 px-3 py-2"
            value={registerForm.soDt}
            onChange={(e) =>
              setRegisterForm({ ...registerForm, soDt: e.target.value })
            }
          />
        </label>
        <label className="block text-sm">
          <span className="mb-1 block text-gray-600">Mật khẩu</span>
          <input
            required
            type="password"
            autoComplete="new-password"
            className="w-full rounded border border-gray-300 px-3 py-2"
            value={registerForm.matKhau}
            onChange={(e) =>
              setRegisterForm({ ...registerForm, matKhau: e.target.value })
            }
          />
        </label>
        <button
          type="submit"
          disabled={loading}
          className="w-full rounded bg-red-600 py-2.5 font-medium text-white hover:bg-red-700 disabled:opacity-60"
        >
          {loading ? 'Đang xử lý...' : 'Đăng ký'}
        </button>
      </form>
    </AuthPageShell>
  )
}
