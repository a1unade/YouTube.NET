import React, { useEffect, useState } from "react";
import axios from "axios";
import { YMaps, Map, Placemark } from "react-yandex-maps";

const YandexMap = ({ apiKey, locationName, snippet, id }) => {
  const [location, setLocation] = useState({
    coordinates: [0, 0],
    zoom: 5,
  });
  if (snippet) {
    locationName = snippet.country;
  }
  useEffect(() => {
    axios
      .get(
        `https://geocode-maps.yandex.ru/1.x/?apikey=${apiKey}&geocode=${locationName}&lang=en_US&format=json`
      )
      .then((response) => {
        const coordinates =
          response.data.response.GeoObjectCollection.featureMember[0].GeoObject.Point.pos
            .split(" ")
            .map((coord) => parseFloat(coord))
            .reverse();
        setLocation({ coordinates, zoom: 3 });
      })
      .catch((error) => {
        console.error(error);
      });
  }, [apiKey, locationName]);
  const handlePlacemarkClick = () => {
    window.location.href = "http://localhost:5173/watch/" + id;
  };
  return (
    <YMaps query={{ apikey: apiKey }}>
      <div className="map-modal">
        <Map
          width="100%"
          height="400px"
          defaultState={{ center: location.coordinates, zoom: location.zoom }}
        >
          <Placemark
            geometry={location.coordinates}
            onClick={handlePlacemarkClick}
          />
        </Map>
      </div>
    </YMaps>
  );
};

export default YandexMap;
