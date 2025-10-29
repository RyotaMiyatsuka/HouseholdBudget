export enum GenreColor {
  Green = 'green',
  Blue = 'blue',
  Red = 'red',
  Yellow = 'yellow',
  Purple = 'purple',
  Pink = 'pink',
  Orange = 'orange',
  Indigo = 'indigo'
}

export interface ColorOption {
  value: GenreColor;
  label: string;
  bgClass: string;
  hoverClass: string;
}

export const GENRE_COLOR_OPTIONS: ColorOption[] = [
  {
    value: GenreColor.Green,
    label: '緑',
    bgClass: 'bg-green-500',
    hoverClass: 'hover:bg-green-600'
  },
  {
    value: GenreColor.Blue,
    label: '青',
    bgClass: 'bg-blue-500',
    hoverClass: 'hover:bg-blue-600'
  },
  {
    value: GenreColor.Red,
    label: '赤',
    bgClass: 'bg-red-500',
    hoverClass: 'hover:bg-red-600'
  },
  {
    value: GenreColor.Yellow,
    label: '黄',
    bgClass: 'bg-yellow-500',
    hoverClass: 'hover:bg-yellow-600'
  },
  {
    value: GenreColor.Purple,
    label: '紫',
    bgClass: 'bg-purple-500',
    hoverClass: 'hover:bg-purple-600'
  },
  {
    value: GenreColor.Pink,
    label: 'ピンク',
    bgClass: 'bg-pink-500',
    hoverClass: 'hover:bg-pink-600'
  },
  {
    value: GenreColor.Orange,
    label: 'オレンジ',
    bgClass: 'bg-orange-500',
    hoverClass: 'hover:bg-orange-600'
  },
  {
    value: GenreColor.Indigo,
    label: '藍',
    bgClass: 'bg-indigo-500',
    hoverClass: 'hover:bg-indigo-600'
  }
];

/**
 * Get Tailwind CSS classes for a genre button based on color and selection state
 */
export function getGenreButtonClasses(color: GenreColor, isSelected: boolean): string {
  const colorOption = GENRE_COLOR_OPTIONS.find(option => option.value === color);

  if (!colorOption) {
    // Fallback to green if color not found
    return isSelected
      ? 'bg-green-500 border-solid border-3 border-white rounded-lg text-white text-lg cursor-pointer aspect-square'
      : 'bg-green-500 hover:bg-green-600 border-0 rounded-lg text-white text-lg cursor-pointer aspect-square';
  }

  const baseClasses = 'rounded-lg text-white text-lg cursor-pointer aspect-square';

  if (isSelected) {
    return `${colorOption.bgClass} border-solid border-3 border-white ${baseClasses}`;
  } else {
    return `${colorOption.bgClass} ${colorOption.hoverClass} border-0 ${baseClasses}`;
  }
}