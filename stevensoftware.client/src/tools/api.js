import axios from 'axios';

const get = async (url, config = {}) => {
  try {
    const response = await axios.get(url, config);

    return {
      success: true,
      data: response.data.data ?? response.data,
      message: response.data.message ?? ''
    };
  } catch (error) {
    return {
      success: false,
      data: null,
      message: error?.response?.data?.message ?? 'Request failed'
    };
  }
};

const post = async (url, data = {}, config = {}) => {
  try {
    const response = await axios.post(url, data, config);

    return {
      success: true,
      data: response.data.data ?? response.data,
      message: response.data.message ?? ''
    };
  } catch (error) {
    return {
      success: false,
      data: null,
      message: error?.response?.data?.message ?? 'Request failed'
    };
  }
};

const _delete = async (url, config = {}) => {
  try {
    const response = await axios.delete(url, config);

    return {
      success: true,
      data: response.data.data ?? response.data,
      message: response.data.message ?? ''
    };
  } catch (error) {
    return {
      success: false,
      data: null,
      message: error?.response?.data?.message ?? 'Request failed'
    };
  }
};

export { get, post, _delete };
