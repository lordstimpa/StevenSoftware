import axios from 'axios';

const get = async (url, config = {}) => {
  try {
    const response = await axios.get(url, config);
    return response.data; // Return only data
  } catch (error) {
    console.error(error);
    if (error.response && error.response.data && error.response.data.message) {
      return { error: true, message: error.response.data.message };
    }
    return null;
  }
};

const post = async (url, data = {}, config = {}) => {
  try {
    const response = await axios.post(url, data, config);
    return response.data; // Return only data
  } catch (error) {
    console.error(error);
    if (error.response && error.response.data && error.response.data.message) {
      return { error: true, message: error.response.data.message };
    }
    return null;
  }
};

const _delete = async (url, config = {}) => {
  try {
    const response = await axios.delete(url, config);
    return response.data; // Return only data
  } catch (error) {
    console.error(error);
    if (error.response && error.response.data && error.response.data.message) {
      return { error: true, message: error.response.data.message };
    }
    return null;
  }
};

export { get, post, _delete };
