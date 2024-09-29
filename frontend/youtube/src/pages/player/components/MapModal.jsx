import React, { useState, useEffect } from "react";
import YandexMap from "./YandexMap";
import apiClient from "../../../utils/apiClient.js";
const MapModal = ({ apiKey, locationName, onClose, channelUrl, id }) => {
  const [owners, setOwner] = useState([]);
  useEffect(() => {
    apiClient
      .get(
        `/channels?part=snippet&part=statistics&part=brandingSettings&part=contentDetails&forHandle=${channelUrl}&maxResults=5`
      )
      .then((response) => {
        setOwner(response.data.items[0]);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  return (
    <div className="map-modal" onClick={(e) => e.stopPropagation()}>
      <div className="map-modal-content">
        <button className="close-button" onClick={onClose}>
          &times;
        </button>
        <YandexMap
          apiKey={apiKey}
          locationName={locationName}
          snippet={owners.snippet}
          id={id}
        />
      </div>
    </div>
  );
};

export default MapModal;
