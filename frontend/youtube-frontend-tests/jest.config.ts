module.exports = {
    preset: 'ts-jest',
    testEnvironment: 'jsdom',
    setupFilesAfterEnv: ['<rootDir>/jest.setup.ts'],
    moduleNameMapper: {
        '\\.(css|scss)$': 'identity-obj-proxy',
    },
    testMatch: ['**/__tests__/**/*.(ts|tsx)', '**/?(*.)+(spec|test).(ts|tsx)'],
    collectCoverage: true,
    collectCoverageFrom: [
        "../youtube-frontend/src/**/*.{ts,tsx}",
        "!../youtube-frontend/src/main.tsx",
        "!**/*.d.ts",
    ],
    coverageDirectory: "<rootDir>/coverage",
    coverageReporters: ["text", "lcov"],
};