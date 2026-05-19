export function mapUserFromApi(content) {
  if (!content) return null

  const maLoai = content.maLoaiNguoiDung || content.MaLoaiNguoiDung || ''
  const role = maLoai === 'QuanTri' ? 'ADMIN' : 'USER'

  return {
    taiKhoan: content.taiKhoan ?? content.TaiKhoan ?? '',
    hoTen: content.hoTen ?? content.HoTen ?? '',
    email: content.email ?? content.Email ?? '',
    soDT: content.soDT ?? content.soDt ?? content.SoDt ?? '',
    maNhom: content.maNhom ?? content.MaNhom ?? '',
    maLoaiNguoiDung: maLoai,
    role,
  }
}
