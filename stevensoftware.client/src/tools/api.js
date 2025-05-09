import axios from 'axios';

const get = async (url, config = {}) => {
  try {
    const response = await axios.get(url, config);
    return response.status === 200 ? response.data : null;
  } catch (error) {
    console.error(error);
    return null;
  }
};

const post = async (url, payload) => {
  try {
    const response = await axios.post(url, payload);
    return response.status === 200 ? response.data : null;
  } catch (error) {
    console.error(error);
    return null;
  }
};

export { get, post };
