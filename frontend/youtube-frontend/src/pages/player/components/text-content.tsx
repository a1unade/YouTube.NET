import React from "react";
import DOMPurify from "dompurify";

interface TextContentProps {
	text: string;
}

const TextContent: React.FC<TextContentProps> = ({ text }) => {
	const formattedText = text
		.replace(/\n/g, "<br>")
		.replace(/(https?:\/\/\S+)/g, '<a href="$1" target="_blank">$1</a>');
	const safeHTML = DOMPurify.sanitize(formattedText);

	return (
		<div
			id={"description"}
			className={"description-text-content"}
			dangerouslySetInnerHTML={{ __html: safeHTML }}
			style={{ whiteSpace: "pre-wrap", textOverflow: "ellipsis" }}
		/>
	);
};

export default TextContent;
