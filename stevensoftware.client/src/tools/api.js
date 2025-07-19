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

const post = async (url, data = {}, config = {}) => {
  try {
    const response = await axios.post(url, data, config);
    return response.data;
  } catch (error) {
    console.error(error);

    if (error.response && error.response.data && error.response.data.message) {
      return { error: true, message: error.response.data.message };
    }

    return { error: true, message: 'An unexpected error occurred.' };
  }
};


const _delete = async (url, config = {}) => {
  try {
    const response = await axios.delete(url, config);
    return response.status === 200 ? response.data : null;
  } catch (error) {
    console.error(error);
    return null;
  }
};

export { get, post, _delete };
