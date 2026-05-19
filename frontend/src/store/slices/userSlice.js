import { createSlice } from '@reduxjs/toolkit'
import {
  loadBookingIdsFromStorage,
  loadTokenFromStorage,
  loadUserFromStorage,
  saveAuthToStorage,
  saveBookingIdsToStorage,
} from '../../utils/authStorage'
import { mapUserFromApi } from '../../utils/mapUser'

const persistedUser = loadUserFromStorage()
const persistedToken = loadTokenFromStorage()
const persistedBookingIds = loadBookingIdsFromStorage()

const initialState = {
  user: persistedUser,
  token: persistedToken,
  bookingIds: persistedBookingIds,
}

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setCredentials(state, action) {
      const { user, token, bookingIds } = action.payload
      state.user = user
      state.token = token
      if (bookingIds !== undefined) {
        state.bookingIds = bookingIds
        saveBookingIdsToStorage(bookingIds)
      }
      saveAuthToStorage(user, token)
    },
    setBookingIds(state, action) {
      state.bookingIds = action.payload
      saveBookingIdsToStorage(action.payload)
    },
  },
})

export const { setCredentials, setBookingIds } = userSlice.actions

/** Sau login: map user + load bookingIds từ localStorage */
export function applyLoginSuccess(dispatch, apiContent) {
  const token = apiContent.accessToken
  const user = mapUserFromApi(apiContent)
  const bookingIds = loadBookingIdsFromStorage()

  dispatch(
    setCredentials({
      user,
      token,
      bookingIds,
    }),
  )

  return { user, token, bookingIds }
}

export default userSlice.reducer
