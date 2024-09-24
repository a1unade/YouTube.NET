// prettier-ignore
module.exports = {
	root: true,
	env: { browser: true, es2022: true },
	extends: [
		"eslint:recommended",
		"plugin:@typescript-eslint/recommended",
		"plugin:react-hooks/recommended",
		"plugin:prettier/recommended",
	],
	ignorePatterns: ["dist", ".eslintrc.cjs"],
	parser: "@typescript-eslint/parser",
	parserOptions: {
		ecmaFeatures: {
			jsx: true,
		},
		ecmaVersion: 2022,
	},
	plugins: ["react-refresh", "@typescript-eslint"],
	rules: {
		"react/jsx-no-target-blank": "off",
		"semi": "error",
		"react-refresh/only-export-components": [
			"warn",
			{ allowConstantExport: true },
		],
		"@typescript-eslint/no-unused-vars": ["warn"],
		"@typescript-eslint/explicit-module-boundary-types": "off",
	},
};
