const TOKEN_CYBERSOFT = import.meta.env.VITE_TOKEN_CYBERSOFT || ''

export function cybersoftHeaders() {
  return {
    TokenCybersoft: TOKEN_CYBERSOFT,
  }
}

export function authHeaders(accessToken) {
  if (!accessToken) return cybersoftHeaders()

  return {
    ...cybersoftHeaders(),
    token: accessToken,
    Authorization: `Bearer ${accessToken}`,
  }
}
