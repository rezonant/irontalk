/*
 * SmalltalkTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 *
 * Permission is granted to copy this document verbatim in any
 * medium, provided that this copyright notice is left intact.
 *
 * Copyright © 2010 William Lahti
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

/**
 * <remarks>A character stream tokenizer.</remarks>
 */
public class SmalltalkTokenizer : Tokenizer {

    /**
     * <summary>Creates a new tokenizer for the specified input
     * stream.</summary>
     *
     * <param name='input'>the input stream to read</param>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    public SmalltalkTokenizer(TextReader input)
        : base(input, false) {

        CreatePatterns();
    }

    /**
     * <summary>Initializes the tokenizer by creating all the token
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        TokenPattern  pattern;

        pattern = new TokenPattern((int) SmalltalkConstants.IDENT,
                                   "IDENT",
                                   TokenPattern.PatternType.REGEXP,
                                   "[a-zA-Z_][a-zA-Z0-9_]*");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.KEYWORD,
                                   "KEYWORD",
                                   TokenPattern.PatternType.REGEXP,
                                   "[a-zA-Z_][a-zA-Z0-9_]*:");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.SELECTOR,
                                   "SELECTOR",
                                   TokenPattern.PatternType.REGEXP,
                                   "([a-zA-Z_][a-zA-Z0-9_]*:?)+");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.STRING,
                                   "STRING",
                                   TokenPattern.PatternType.REGEXP,
                                   "('.*[^\\\\]')");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.DOC,
                                   "DOC",
                                   TokenPattern.PatternType.REGEXP,
                                   "(\".*[^\\\\]\")");
        pattern.Ignore = true;
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.CHAR_LITERAL,
                                   "CHAR_LITERAL",
                                   TokenPattern.PatternType.REGEXP,
                                   "\\$.");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.NUM_LITERAL,
                                   "NUM_LITERAL",
                                   TokenPattern.PatternType.REGEXP,
                                   "[0-9]+|[0-9]*\\.[0-9]+");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.LINE_IGNORE,
                                   "LINE_IGNORE",
                                   TokenPattern.PatternType.REGEXP,
                                   "(//.*$)");
        pattern.Ignore = true;
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.IGNORE,
                                   "IGNORE",
                                   TokenPattern.PatternType.REGEXP,
                                   "/\\*([^*]|\\*[^/])*\\*/");
        pattern.Ignore = true;
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.S,
                                   "S",
                                   TokenPattern.PatternType.REGEXP,
                                   "[ \\t\\n\\r\\f]+");
        pattern.Ignore = true;
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.HASH,
                                   "HASH",
                                   TokenPattern.PatternType.STRING,
                                   "#");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.LEFT_PAREN,
                                   "LEFT_PAREN",
                                   TokenPattern.PatternType.STRING,
                                   "(");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.RIGHT_PAREN,
                                   "RIGHT_PAREN",
                                   TokenPattern.PatternType.STRING,
                                   ")");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.LEFT_BRACKET,
                                   "LEFT_BRACKET",
                                   TokenPattern.PatternType.STRING,
                                   "[");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.RIGHT_BRACKET,
                                   "RIGHT_BRACKET",
                                   TokenPattern.PatternType.STRING,
                                   "]");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.LEFT_BRACE,
                                   "LEFT_BRACE",
                                   TokenPattern.PatternType.STRING,
                                   "{");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.RIGHT_BRACE,
                                   "RIGHT_BRACE",
                                   TokenPattern.PatternType.STRING,
                                   "}");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.COLON,
                                   "COLON",
                                   TokenPattern.PatternType.STRING,
                                   ":");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.SEMI_COLON,
                                   "SEMI_COLON",
                                   TokenPattern.PatternType.STRING,
                                   ";");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.VAR_DELIM,
                                   "VAR_DELIM",
                                   TokenPattern.PatternType.STRING,
                                   "|");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.DOT,
                                   "DOT",
                                   TokenPattern.PatternType.STRING,
                                   ".");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.BINARY,
                                   "BINARY",
                                   TokenPattern.PatternType.REGEXP,
                                   "[,=~-\\+/\\*!@\\$%^&<>\\?]+");
        AddPattern(pattern);

        pattern = new TokenPattern((int) SmalltalkConstants.ASSIGN,
                                   "ASSIGN",
                                   TokenPattern.PatternType.STRING,
                                   ":=");
        AddPattern(pattern);
    }
}
