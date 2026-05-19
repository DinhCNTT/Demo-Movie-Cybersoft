import {
  STORAGE_BOOKING_IDS,
  STORAGE_TOKEN,
  STORAGE_USER,
} from '../constants/storageKeys'

export function loadUserFromStorage() {
  try {
    const raw = localStorage.getItem(STORAGE_USER)
    return raw ? JSON.parse(raw) : null
  } catch {
    return null
  }
}

export function loadTokenFromStorage() {
  return localStorage.getItem(STORAGE_TOKEN) || null
}

export function loadBookingIdsFromStorage() {
  try {
    const raw = localStorage.getItem(STORAGE_BOOKING_IDS)
    const parsed = raw ? JSON.parse(raw) : []
    return Array.isArray(parsed) ? parsed : []
  } catch {
    return []
  }
}

export function saveAuthToStorage(user, token) {
  localStorage.setItem(STORAGE_USER, JSON.stringify(user))
  localStorage.setItem(STORAGE_TOKEN, token)
}

export function saveBookingIdsToStorage(bookingIds) {
  localStorage.setItem(STORAGE_BOOKING_IDS, JSON.stringify(bookingIds))
}

export function logout() {
  localStorage.removeItem(STORAGE_USER)
  localStorage.removeItem(STORAGE_TOKEN)
  localStorage.removeItem(STORAGE_BOOKING_IDS)
  window.location.href = '/'
}
