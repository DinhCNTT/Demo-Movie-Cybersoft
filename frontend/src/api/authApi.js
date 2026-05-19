import http from './http'
import { authHeaders, cybersoftHeaders } from './headers'

export async function loginApi({ taiKhoan, matKhau }) {
  const { data } = await http.post(
    '/QuanLyNguoiDung/DangNhap',
    { taiKhoan, matKhau },
    { headers: cybersoftHeaders() },
  )
  return data
}

export async function registerApi(payload) {
  const { data } = await http.post(
    '/QuanLyNguoiDung/DangKy',
    payload,
    { headers: cybersoftHeaders() },
  )
  return data
}

export async function fetchAccountInfo(accessToken) {
  const { data } = await http.post(
    '/QuanLyNguoiDung/ThongTinTaiKhoan',
    null,
    { headers: authHeaders(accessToken) },
  )
  return data
}
