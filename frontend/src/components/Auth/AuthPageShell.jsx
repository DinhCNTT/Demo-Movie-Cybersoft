import { Link } from 'react-router-dom'

const DEFAULT_BG =
  "https://wallpapers.com/images/featured/movie-9pvmdtvz4cb0xl37.jpg";

const BG =
  import.meta.env.VITE_AUTH_BG_URL?.trim() || DEFAULT_BG

export default function AuthPageShell({ title, children }) {
  return (
    <div className="relative isolate min-h-[calc(100dvh-5.5rem)] w-full overflow-hidden">
      <div
        className="absolute inset-0 bg-cover bg-center"
        style={{ backgroundImage: `url(${BG})` }}
        aria-hidden
      />
      <div className="absolute inset-0 bg-black/40" aria-hidden />

      <div className="relative z-10 flex min-h-[calc(100dvh-5.5rem)] items-center justify-center px-4 py-10">
        <div className="w-full max-w-md rounded-xl bg-white shadow-2xl ring-1 ring-black/5">
          <div className="flex items-center justify-between border-b border-gray-100 px-6 py-4">
            <h1 className="text-lg font-semibold text-gray-900">{title}</h1>
            <Link
              to="/"
              className="text-2xl leading-none text-gray-400 hover:text-gray-600"
              aria-label="Đóng và về trang chủ"
            >
              ×
            </Link>
          </div>
          <div className="px-6 py-5">{children}</div>
        </div>
      </div>
    </div>
  );
}
