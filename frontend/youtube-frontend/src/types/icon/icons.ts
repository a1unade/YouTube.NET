import React from 'react';

export type Icon = {
  default: React.ReactNode;
  filled: React.ReactNode;
};

export type IconMapping = {
  [key: string]: Icon;
};
