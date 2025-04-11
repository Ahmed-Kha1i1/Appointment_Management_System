// authSlice.js
import { createSlice } from "@reduxjs/toolkit";
import {
  loadFromLocalStorage,
  removeFromLocalStorage,
  saveToLocalStorage,
} from "../Utils/localStorageUtils";

const sessionStorageKey = "session";

function loadSession() {
  const session = loadFromLocalStorage(sessionStorageKey);
  if (!session ) {
    return {
      userId: null,
      roleId: null,
      expiresOn: null,
      accessToken: null,
      isAuthenticated: false,
      loading: false
    };
  }

  return session;
}

const initialState = loadSession();

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setLoading: (state, action) => {
      state.loading = action.payload;
      saveToLocalStorage(sessionStorageKey, state);
    },
    setSession: (state, action) => {
      const { userId,roleId, expiresOn, accessToken } = action.payload;
      state.userId = userId;
      state.roleId = roleId;
      state.expiresOn = expiresOn;
      state.accessToken = accessToken;
      state.isAuthenticated = true;
      state.loading = false;

      saveToLocalStorage(sessionStorageKey, state);
    },
    clearSession: (state) => {
      state.userId = null;
      state.roleId = null;
      state.expiresOn = null;
      state.accessToken = null;
      state.isAuthenticated = false;
      state.loading = false;

      removeFromLocalStorage(sessionStorageKey);
    },
  },
});

export const { setLoading, setSession, clearSession } = authSlice.actions;

export default authSlice.reducer;
