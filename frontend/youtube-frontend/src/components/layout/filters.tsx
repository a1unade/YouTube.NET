import { useState } from 'react';
import { FiltersProps } from '../../interfaces/filter/filter-props.ts';

/**
 * Компонент фильтров.
 *
 * Отображает набор кнопок, позволяющих выбрать один из доступных фильтров.
 *
 * @param {Object} props - Свойства компонента.
 * @param {FiltersProps} props.filters - Массив объектов фильтров, каждый из которых содержит id и название.
 *
 * @returns {JSX.Element} Возвращает элемент интерфейса с кнопками фильтров.
 *
 * @example Пример использования:
 *   <Filters filters={[{ id: 1, name: 'Фильтр 1' }, { id: 2, name: 'Фильтр 2' }]} />
 */

const Filters = ({ filters }: FiltersProps): JSX.Element => {
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
