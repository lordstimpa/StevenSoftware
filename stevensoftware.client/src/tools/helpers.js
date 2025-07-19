  const formatDateTime = (timestamp) => {
    if (!timestamp) return '';

    const date = new Date(timestamp);

    const options = {
      year: 'numeric',
      month: 'short',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,
    };

    return date.toLocaleString('sv-SE', options).replace(',', ' -');
  }

  export {
    formatDateTime
  };