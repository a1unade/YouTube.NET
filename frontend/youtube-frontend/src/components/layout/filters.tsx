import { useState } from 'react';
import { FiltersProps } from '../../interfaces/filter/filter-props.ts';

const Filters = ({ filters }: FiltersProps) => {
  const [selectedFilter, setSelectedFilter] = useState<number>(0);

  return (
    <div className="filter-button-container">
      {filters.map((filter) => (
        <button
          key={filter.id}
          onClick={() => setSelectedFilter(filter.id)}
          className={`filter-button ${selectedFilter === filter.id ? 'selected' : ''}`}
        >
          {filter.name}
        </button>
      ))}
    </div>
  );
};

export default Filters;
