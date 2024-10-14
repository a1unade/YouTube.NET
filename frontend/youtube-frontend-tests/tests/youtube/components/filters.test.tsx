import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Filters from '../../../temp-src/components/layout/filters.tsx';

describe('Component Filters', () => {
    const filters = [
        { id: 1, name: 'Фильтр 1' },
        { id: 2, name: 'Фильтр 2' },
        { id: 3, name: 'Фильтр 3' },
    ];

    beforeEach(() => {
        render(<Filters filters={filters} />);
    });

    it('правильно рендерит кнопки фильтров', () => {
        filters.forEach(filter => {
            expect(screen.getByRole('button', { name: filter.name })).toBeInTheDocument();
        });
    });

    it('устанавливает выбранный фильтр при клике на кнопку', () => {
        const filterButton1 = screen.getByRole('button', { name: 'Фильтр 1' });

        fireEvent.click(filterButton1);

        expect(filterButton1).toHaveClass('selected');

        const filterButton2 = screen.getByRole('button', { name: 'Фильтр 2' });

        fireEvent.click(filterButton2);

        expect(filterButton2).toHaveClass('selected');
        expect(filterButton1).not.toHaveClass('selected');
    });

    it('не выбирает кнопку изначально', () => {
        filters.forEach(filter => {
            expect(screen.getByRole('button', { name: filter.name })).not.toHaveClass('selected');
        });
    });
});
