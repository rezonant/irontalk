/*
 * SmalltalkParser.cs
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
 * <remarks>A token stream parser.</remarks>
 */
public class SmalltalkParser : RecursiveDescentParser {

    /**
     * <summary>An enumeration with the generated production node
     * identity constants.</summary>
     */
    private enum SynteticPatterns {
        SUBPRODUCTION_1 = 3001,
        SUBPRODUCTION_2 = 3002,
        SUBPRODUCTION_3 = 3003,
        SUBPRODUCTION_4 = 3004,
        SUBPRODUCTION_5 = 3005,
        SUBPRODUCTION_6 = 3006,
        SUBPRODUCTION_7 = 3007,
        SUBPRODUCTION_8 = 3008,
        SUBPRODUCTION_9 = 3009,
        SUBPRODUCTION_10 = 3010
    }

    /**
     * <summary>Creates a new parser with a default analyzer.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public SmalltalkParser(TextReader input)
        : base(input) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new parser.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <param name='analyzer'>the analyzer to parse with</param>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    public SmalltalkParser(TextReader input, SmalltalkAnalyzer analyzer)
        : base(input, analyzer) {

        CreatePatterns();
    }

    /**
     * <summary>Creates a new tokenizer for this parser. Can be overridden
     * by a subclass to provide a custom implementation.</summary>
     *
     * <param name='input'>the input stream to read from</param>
     *
     * <returns>the tokenizer created</returns>
     *
     * <exception cref='ParserCreationException'>if the tokenizer
     * couldn't be initialized correctly</exception>
     */
    protected override Tokenizer NewTokenizer(TextReader input) {
        return new SmalltalkTokenizer(input);
    }

    /**
     * <summary>Initializes the parser by creating all the production
     * patterns.</summary>
     *
     * <exception cref='ParserCreationException'>if the parser
     * couldn't be initialized correctly</exception>
     */
    private void CreatePatterns() {
        ProductionPattern             pattern;
        ProductionPatternAlternative  alt;

        pattern = new ProductionPattern((int) SmalltalkConstants.SEQUENCE,
                                        "sequence");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.VAR_DEF, 0, 1);
        alt.AddProduction((int) SmalltalkConstants.STATEMENT, 1, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.STATEMENT,
                                        "statement");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.DOT, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.EXPRESSION, 1, 1);
        alt.AddToken((int) SmalltalkConstants.DOT, 0, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.SYMBOL_LITERAL,
                                        "symbol_literal");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.HASH, 1, 1);
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_1, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.VAR_DEF,
                                        "var_def");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.VAR_DELIM, 1, 1);
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, -1);
        alt.AddToken((int) SmalltalkConstants.VAR_DELIM, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.BLOCK_PARAMS,
                                        "block_params");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_3, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.BLOCK_LITERAL,
                                        "block_literal");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.LEFT_BRACKET, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.BLOCK_PARAMS, 0, 1);
        alt.AddProduction((int) SmalltalkConstants.SEQUENCE, 0, 1);
        alt.AddToken((int) SmalltalkConstants.RIGHT_BRACKET, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.ARRAY_LITERAL,
                                        "array_literal");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.LEFT_BRACE, 1, 1);
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_5, 0, 1);
        alt.AddToken((int) SmalltalkConstants.RIGHT_BRACE, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.WORD_ARRAY_LITERAL,
                                        "word_array_literal");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.HASH, 1, 1);
        alt.AddToken((int) SmalltalkConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_7, 0, -1);
        alt.AddToken((int) SmalltalkConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.RECEIVER,
                                        "receiver");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.SYMBOL_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.STRING, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.NUM_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.CHAR_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.WORD_ARRAY_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.BLOCK_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.ARRAY_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.LEFT_PAREN, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.EXPRESSION, 1, 1);
        alt.AddToken((int) SmalltalkConstants.RIGHT_PAREN, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.BINARY_SEND,
                                        "binary_send");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.BINARY, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.RECEIVER, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.UNARY_SEND, 0, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.KEYWORD_SEND,
                                        "keyword_send");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_8, 1, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.EXPRESSION,
                                        "expression");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.RECEIVER, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.MESSAGE, 0, -1);
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_9, 0, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.UNARY_SEND,
                                        "unary_send");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.SIMPLE_SEND,
                                        "simple_send");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.UNARY_SEND, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.BINARY_SEND, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.ASSIGN_SEND,
                                        "assign_send");
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.ASSIGN, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.EXPRESSION, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SmalltalkConstants.MESSAGE,
                                        "message");
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_10, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_1,
                                        "Subproduction1");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.BINARY, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.SELECTOR, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_2,
                                        "Subproduction2");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.COLON, 1, 1);
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_3,
                                        "Subproduction3");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_2, 1, -1);
        alt.AddToken((int) SmalltalkConstants.VAR_DELIM, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_4,
                                        "Subproduction4");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.DOT, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.EXPRESSION, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_5,
                                        "Subproduction5");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.EXPRESSION, 1, 1);
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_4, 0, -1);
        alt.AddToken((int) SmalltalkConstants.DOT, 0, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_6,
                                        "Subproduction6");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.WORD_ARRAY_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_7,
                                        "Subproduction7");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.IDENT, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.STRING, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.NUM_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.CHAR_LITERAL, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.HASH, 1, 1);
        alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_6, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_8,
                                        "Subproduction8");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.KEYWORD, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.RECEIVER, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.SIMPLE_SEND, 0, -1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_9,
                                        "Subproduction9");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddToken((int) SmalltalkConstants.SEMI_COLON, 1, 1);
        alt.AddProduction((int) SmalltalkConstants.MESSAGE, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);

        pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_10,
                                        "Subproduction10");
        pattern.Synthetic = true;
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.SIMPLE_SEND, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.KEYWORD_SEND, 1, 1);
        pattern.AddAlternative(alt);
        alt = new ProductionPatternAlternative();
        alt.AddProduction((int) SmalltalkConstants.ASSIGN_SEND, 1, 1);
        pattern.AddAlternative(alt);
        AddPattern(pattern);
    }
}
