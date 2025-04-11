export function saveToLocalStorage(key, value) {
  try {
    const serializedObj = JSON.stringify(value);

    localStorage.setItem(key, serializedObj);
  } catch (err) {
    console.error("Could not save to local storage", err);
  }
}

export function loadFromLocalStorage(key) {
  const serializedObj = localStorage.getItem(key);
  return serializedObj ? JSON.parse(serializedObj) : undefined;
}

export function removeFromLocalStorage(key) {
  const serializedObj = localStorage.removeItem(key);
  return serializedObj ? JSON.parse(serializedObj) : undefined;
}
