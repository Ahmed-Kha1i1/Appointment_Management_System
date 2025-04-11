import { BASE_URL, sessoin } from "../Settings/Constants";
import { clearSession, setSession } from "../Slices/authSlice";
import store from "../Slices/store";

// Flag to indicate if a token refresh is in progress
let isRefreshing = false;

// Queue to hold requests that failed due to 401 errors
let failedQueue = [];

/**
 * Fetch data from a given URL with specified options.
 * Handles token refresh if a 401 error is encountered.
 *
 * @param {string} url - The URL to fetch data from.
 * @param {object} options - Fetch options, including headers.
 * @returns {Promise<any>} - The response data.
 */
export default async function fetchData(url, options = {}) {
  try {
    options.credentials = "include";
    setToken(options);

    let res = await fetch(url, options);

    // If the response status is 401, add the request to the queue to retry after token refresh
    if (res.status === 401 && !options._retry) {
      return new Promise((resolve, reject) => {
        addToQueue(() => fetchData(url, options).then(resolve).catch(reject));
        if (!isRefreshing) {
          refershTokentry();
        }
      });
    }

    const data = await res.json?.();

    if (!res.ok) throw new Error(data?.message || "An error occurred");

    return data;
  } catch (error) {
    throw new Error(error.message);
  }
}

/**
 * Set the authorization token in the request headers.
 *
 * @param {object} options - Fetch options, including headers.
 */
function setToken(options) {
  const sessionData = localStorage.getItem(sessoin);
  let accessToken;
  if (sessionData) {
    const sessionObject = JSON.parse(sessionData);

    accessToken = sessionObject.accessToken;
  }

  if (accessToken)
    options.headers = {
      ...options.headers,
      Authorization: `Bearer ${accessToken}`,
    };

  
}

/**
 * Add a callback to the failed request queue.
 *
 * @param {function} callback - The function to execute after token refresh.
 */
function addToQueue(callback) {
  failedQueue.push(callback);
}

/**
 * Refresh the authentication token.
 * Retries failed requests after a successful token refresh.
 */
async function refershTokentry() {
  isRefreshing = true;

  try {
    let res = await fetch(`${BASE_URL}/Auth/RefreshToken`, {
      method: "POST",
      credentials: "include"
    });

    const result = await res.json();

    const newSessoin = result.data;

    if (!newSessoin) {
      window.location.href = "/signin";
      store.dispatch(clearSession());
      return;
    }

    store.dispatch(setSession(newSessoin));
    processQueue(null, newSessoin.accessToken);
  } catch (error) {
    store.dispatch(clearSession());
    processQueue(error, null);

  } finally {
    isRefreshing = false;
  }
}

/**
 * Process the queue of failed requests.
 *
 * @param {Error|null} error - The error, if any, from the token refresh.
 * @param {string|null} token - The new access token, if available.
 */
function processQueue(error, token = null) {
  failedQueue.forEach((callback) => {
    if (error) {
      callback(Promise.reject(error));
    } else {
      callback(Promise.resolve(token));
    }
  });

  failedQueue = [];
}
