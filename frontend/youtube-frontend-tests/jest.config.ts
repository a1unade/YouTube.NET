// @ts-ignore
module.exports = {
    preset: 'ts-jest',
    testEnvironment: 'jsdom',
    setupFilesAfterEnv: ['<rootDir>/jest.setup.ts'],
    moduleNameMapper: {
        '^src/(.*)$': '<rootDir>/temp-src/$1',
    },
    testMatch: ['**/__tests__/**/*.(ts|tsx)', '**/?(*.)+(spec|test).(ts|tsx)'],
    collectCoverage: true,
    collectCoverageFrom: [
        "temp-src/**/*.{ts,tsx}",
        "!temp-src/main.tsx",
        "!temp-src/assets/icons.tsx",
        "!temp-src/types/",
        "!temp-src/**/*.d.ts",
        "acc-src/**/*.{ts,tsx}",
        "!acc-src/main.tsx",
        "!acc-src/**/*.d.ts",
        "!**/icons.tsx",
    ],

    coverageDirectory: "<rootDir>/coverage",
    coverageReporters: ["text", "lcov"],
    testPathIgnorePatterns: ["/node_modules/", "./temp-src/", "./acc-src"],
};