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
        "!**/*.d.ts",
    ],
    coverageDirectory: "<rootDir>/coverage",
    coverageReporters: ["text", "lcov"],
    testPathIgnorePatterns: ["/node_modules/", "./temp-src/"],
};